import type { Generated, Selectable, Insertable, Updateable, ColumnType } from 'kysely';

export interface Database {
	tracker: TrackerTable;
	reading: ReadingTable;
}

export interface TrackerTable {
	id: Generated<number>;
	name: string;
	uuid: Generated<string>;
	status: Generated<'active' | 'archived'>;
	sample_type: 'random' | 'open';
	color: string | null;
	possible_values: ColumnType<string[],string[] | string,string[] | string> | null;
	created_at: Generated<Date>
	// updated_at: Generated<Date>
}

export type Tracker = Selectable<TrackerTable>
export type NewTracker = Insertable<TrackerTable>
export type TrackerUpdate = Updateable<TrackerTable>

export interface ReadingTable {
	id: Generated<number>;
	value: string;
	tracker_id: number;
	created_at: Generated<Date>
	// updated_at: Generated<Date>
}

export type Reading = Selectable<ReadingTable>
export type NewReading = Insertable<ReadingTable>
export type ReadingUpdate = Updateable<ReadingTable>
