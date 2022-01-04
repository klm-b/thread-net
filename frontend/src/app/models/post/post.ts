import { User } from '../user';
import { Comment } from '../comment/comment';
import { Reaction } from '../reactions/reaction';

export interface Post {
    id: number;
    createdAt: Date;
    isUpdated: boolean;
    author: User;
    previewImage: string;
    body: string;
    comments: Comment[];
    reactions: Reaction[];
}
