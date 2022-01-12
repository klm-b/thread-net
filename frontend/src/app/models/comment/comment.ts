import { User } from '../user';
import { Reaction } from '../reactions/reaction';

export interface Comment {
    id: number;
    createdAt: Date;
    isUpdated: boolean;
    author: User;
    body: string;
    likesNumber: number;
    dislikesNumber: number;
    isLikedByMe: boolean | null;
}
