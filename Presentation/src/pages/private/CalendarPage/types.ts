export type ActivityType = 'sport' | 'meeting' | 'education' | 'other';
export type ActivityStatus = 'planned' | 'inProgress' | 'completed' | 'cancelled';

export interface Activity {
  id: string;
  name: string;
  description: string;
  type: number;
  status: number;
  startAt: Date;
  endAt: Date | null;
  groupId: string;
  activityMembers: ActivityMember[]
}

export interface ActivityMember {
  id: string,
  activityId: string,
  groupMemberId: string
}