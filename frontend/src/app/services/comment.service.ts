import { Injectable } from '@angular/core';
import { HttpInternalService } from './http-internal.service';
import { NewComment } from '../models/comment/new-comment';
import { Comment } from '../models/comment/comment';
import { UpdateComment } from '../models/comment/update-comment';

@Injectable({ providedIn: 'root' })
export class CommentService {
    public routePrefix = '/api/comments';

    constructor(private httpService: HttpInternalService) {}

    public createComment(post: NewComment) {
        return this.httpService.postFullRequest<Comment>(`${this.routePrefix}`, post);
    }

    public getCommentsToPost(postId: number) {
        return this.httpService.getFullRequest<Comment[]>(`${this.routePrefix}/${postId}`);
    }

    public updateComment(comment: UpdateComment) {
        return this.httpService.putFullRequest<Comment>(`${this.routePrefix}`, comment);
    }
}
