import { Injectable } from '@angular/core';
import { AuthenticationService } from './auth.service';
import { Post } from '../models/post/post';
import { NewReaction } from '../models/reactions/newReaction';
import { PostService } from './post.service';
import { User } from '../models/user';
import { map, catchError } from 'rxjs/operators';
import { Observable, of } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class ReactionService {
    public constructor(private authService: AuthenticationService, private postService: PostService) {}

    public reactToPost(post: Post, currentUser: User, newReaction: boolean) {
        const innerPost = post;
        const currentReaction = innerPost.isLikedByMe;
        const currentlikesNumber = innerPost.likesNumber;
        const currentdislikesNumber = innerPost.dislikesNumber;

        let action: Observable<any> = currentReaction === newReaction
            ? this.postService.deleteReaction({
                entityId: innerPost.id,
                userId: currentUser.id
            })
            : this.postService.likePost({
                entityId: innerPost.id,
                isLike: newReaction,
                userId: currentUser.id
            });


        // update current post instantly
        if (currentReaction === newReaction) {

            innerPost.isLikedByMe = null;

            if (newReaction)
                innerPost.likesNumber--;
            else
                innerPost.dislikesNumber--;
        }
        else {

            if (newReaction) {
                innerPost.likesNumber++;
                if (currentReaction === false)
                    innerPost.dislikesNumber = innerPost.dislikesNumber === 0 ? 0 : innerPost.dislikesNumber - 1;
            } else {
                innerPost.dislikesNumber++;
                if (currentReaction)
                    innerPost.likesNumber = innerPost.likesNumber === 0 ? 0 : innerPost.likesNumber - 1;
            }

            innerPost.isLikedByMe = newReaction;
        }

        return action.pipe(
            map(() => innerPost),
            catchError(() => {
                // revert current changes in case of any error
                innerPost.isLikedByMe = currentReaction;
                innerPost.likesNumber = currentlikesNumber;
                innerPost.dislikesNumber = currentdislikesNumber;
                return of(innerPost);
            })
        );
    }
}
