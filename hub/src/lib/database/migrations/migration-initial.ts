import type { Kysely } from 'kysely';
import { addStandardColumns } from '$lib/database/migration-helpers';

export async function up(db: Kysely<any>): Promise<void> {
	await db.schema
		.createTable('tracker')
		.$call(addStandardColumns)
		.addColumn('name', 'text', (col) => col.notNull())
		.addColumn('uuid', 'text', (col) => col.notNull())
		.addColumn('status', 'text', (col) => col.notNull().defaultTo('active'))
		.addColumn('sample_type', 'text')
		.addColumn('color', 'text')
		.addColumn('possible_values', 'text')
		.execute();

	await db.schema
		.createTable('reading')
		.$call(addStandardColumns)
		.addColumn('value', 'text', (col) => col.notNull())
		.addColumn('tracker_id', 'integer', (col) => col.notNull().references('tracker.id').onDelete('cascade'))
		.execute();
}

export async function down(db: Kysely<any>): Promise<void> {
	await db.schema.dropTable('tracker').execute();
}
