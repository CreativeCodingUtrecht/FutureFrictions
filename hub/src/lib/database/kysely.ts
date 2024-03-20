import {
	Kysely,
	Migrator,
	SqliteAdapter,
	SqliteDialect,
	SqliteIntrospector,
	SqliteQueryCompiler,
	DummyDriver,
	type LogConfig
} from 'kysely';
import { SerializeJSONPlugin } from './serialize-json-plugin';
import { browser } from '$app/environment';
import { StaticMigrationProvider } from './migration-provider';
import type { Database } from './types';

const SQLITE_WASM_PERSIST = true;

const dummyDialect = {
	createAdapter: () => new SqliteAdapter(),
	createDriver: () => new DummyDriver(),
	createIntrospector: (db: Kysely<any>) => new SqliteIntrospector(db),
	createQueryCompiler: () => new SqliteQueryCompiler()
};

const sqliteDialect: SqliteDialect = new SqliteDialect({
	async database() {
		console.log('Initializing SQLite Dialect (better-sqlite3)');

		const SQLite = (await import('better-sqlite3')).default;
		return new SQLite(':memory:');
	}
});



const isVitest = () => {
	return process && process.env.TEST === 'true';
};

const configureDialect = () => {
	// Mobile platforms
	if (Capacitor.isNativePlatform()) {
		return capacitorDialect;
	}

	// Browser
	if (browser) return wasmDialect;

	// Vitest
	if (isVitest()) {
		return sqliteDialect;
	}

	// Vite server or Vite build
	return dummyDialect;
};

const configureLog = (): LogConfig => {
	// Mobile platforms and browser
	if (Capacitor.isNativePlatform() || browser || isVitest()) {
		return ['query', 'error'];
	}

	// Vite server or Vite build
	return [];
};

const db = new Kysely<Database>({
	dialect: configureDialect(),
	log: configureLog(),
	plugins: [new SerializeJSONPlugin()]
});

const migrateToLatest = async () => {
	const migrator = new Migrator({
		db,
		provider: new StaticMigrationProvider()
	});

	const { error, results } = await migrator.migrateToLatest();

	results?.forEach((it) => {
		if (it.status === 'Success') {
			console.log(`migration "${it.migrationName}" was executed successfully`);
		} else if (it.status === 'Error') {
			console.error(`failed to execute migration "${it.migrationName}"`);
		}
	});

	if (error) {
		console.error('failed to migrate', error);
	}

	// const tables = await db.introspection.getTables();
	// console.log('Schema after migration:', tables);

	return db;
};

migrateToLatest();

export default db;
