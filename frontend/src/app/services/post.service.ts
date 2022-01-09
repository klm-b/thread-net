import { Injectable } from '@angular/core';
import { HttpInternalService } from './http-internal.service';
import { Post } from '../models/post/post';
import { NewReaction } from '../models/reactions/newReaction';
import { NewPost } from '../models/post/new-post';
import { UpdatePost } from '../models/post/update-post';
import { DeleteReaction } from '../models/reactions/deleteReaction';

@Injectable({ providedIn: 'root' })
export class PostService {
    public routePrefix = '/api/posts';

    constructor(private httpService: HttpInternalService) {}

    public getPosts() {
        return this.httpService.getFullRequest<Post[]>(`${this.routePrefix}`);
    }

    public createPost(post: NewPost) {
        return this.httpService.postFullRequest<Post>(`${this.routePrefix}`, post);
    }

    public updatePost(post: UpdatePost) {
        return this.httpService.putFullRequest<void>(`${this.routePrefix}`, post);
    }

    public deletePost(id: number) {
        return this.httpService.deleteFullRequest<void>(`${this.routePrefix}/${id}`);
    }

    public likePost(reaction: NewReaction) {
        return this.httpService.postFullRequest<Post>(`${this.routePrefix}/reactions`, reaction);
    }

    public deleteReaction(reaction: DeleteReaction) {
        return this.httpService.deleteFullRequest<void>(`${this.routePrefix}/reactions`, reaction);
    }
}
