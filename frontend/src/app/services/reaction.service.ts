import { Injectable } from '@angular/core';
import { AuthenticationService } from './auth.service';
import { Post } from '../models/post/post';
import { Comment } from '../models/comment/comment';
import { PostService } from './post.service';
import { User } from '../models/user';
import { map, catchError } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { CommentService } from './comment.service';

@Injectable({ providedIn: 'root' })
export class ReactionService {
    public constructor(private authService: AuthenticationService, private postService: PostService, private commentService: CommentService) {}

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

    public reactToComment(comment: Comment, currentUser: User, newReaction: boolean) {
        const innerComment = comment;
        const currentReaction = innerComment.isLikedByMe;
        const currentlikesNumber = innerComment.likesNumber;
        const currentdislikesNumber = innerComment.dislikesNumber;

        let action: Observable<any> = currentReaction === newReaction
            ? this.commentService.deleteReaction({
                entityId: innerComment.id,
                userId: currentUser.id
            })
            : this.commentService.likeComment({
                entityId: innerComment.id,
                isLike: newReaction,
                userId: currentUser.id
            });


        // update current post instantly
        if (currentReaction === newReaction) {

            innerComment.isLikedByMe = null;

            if (newReaction)
                innerComment.likesNumber--;
            else
                innerComment.dislikesNumber--;
        }
        else {

            if (newReaction) {
                innerComment.likesNumber++;
                if (currentReaction === false)
                    innerComment.dislikesNumber = innerComment.dislikesNumber === 0 ? 0 : innerComment.dislikesNumber - 1;
            } else {
                innerComment.dislikesNumber++;
                if (currentReaction)
                    innerComment.likesNumber = innerComment.likesNumber === 0 ? 0 : innerComment.likesNumber - 1;
            }

            innerComment.isLikedByMe = newReaction;
        }

        return action.pipe(
            map(() => innerComment),
            catchError(() => {
                // revert current changes in case of any error
                innerComment.isLikedByMe = currentReaction;
                innerComment.likesNumber = currentlikesNumber;
                innerComment.dislikesNumber = currentdislikesNumber;
                return of(innerComment);
            })
        );
    }
}
