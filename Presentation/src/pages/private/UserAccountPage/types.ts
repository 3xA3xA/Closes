import type { User } from "../../../auth/types";

export interface EditedUser extends Pick<User, 'id' | 'token'> {
    newPassword: string | null | undefined;
    avatar: FileList | null | undefined
}

export interface Member {
    id: string,
    userId: string,
    groupId: string,
    joinedAt: string,
    role: number,
    uniqueColor: string,
    userName: string
}

export interface Group {
    id: string;
    name: string;
    type: number;
    code: string;
    createdAt: string;
    members: Member[]
}