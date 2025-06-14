import type { User } from "../../../auth/types";

export interface EditedUser extends Pick<User, 'id' | 'token'> {
    newPassword: string | null | undefined;
    avatar: FileList | null | undefined
}