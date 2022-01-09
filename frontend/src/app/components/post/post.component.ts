import { Component, EventEmitter, Input, OnDestroy, Output } from '@angular/core';
import { Post } from '../../models/post/post';
import { AuthenticationService } from '../../services/auth.service';
import { AuthDialogService } from '../../services/auth-dialog.service';
import { empty, Observable, Subject } from 'rxjs';
import { DialogType } from '../../models/common/auth-dialog-type';
import { ReactionService } from '../../services/reaction.service';
import { NewComment } from '../../models/comment/new-comment';
import { CommentService } from '../../services/comment.service';
import { User } from '../../models/user';
import { Comment } from '../../models/comment/comment';
import { catchError, switchMap, takeUntil } from 'rxjs/operators';
import { SnackBarService } from '../../services/snack-bar.service';
import { PostDialogsService } from 'src/app/services/post-dialogs.service';
import { UpdatePost } from 'src/app/models/post/update-post';

@Component({
    selector: 'app-post',
    templateUrl: './post.component.html',
    styleUrls: ['./post.component.sass']
})
export class PostComponent implements OnDestroy {
    @Input() public post: Post;
    @Input() public currentUser: User;

    public showComments = false;
    public newComment = {} as NewComment;

    private unsubscribe$ = new Subject<void>();

    @Output() onDelete = new EventEmitter<number>();

    public constructor(
        private authService: AuthenticationService,
        private authDialogService: AuthDialogService,
        private postDialogsService: PostDialogsService,
        private reactionsService: ReactionService,
        private commentService: CommentService,
        private snackBarService: SnackBarService
    ) {}

    public ngOnDestroy() {
        this.unsubscribe$.next();
        this.unsubscribe$.complete();
    }

    public toggleComments() {
        if (!this.currentUser) {
            this.catchErrorWrapper(this.authService.getUser())
                .pipe(takeUntil(this.unsubscribe$))
                .subscribe((user) => {
                    if (user) {
                        this.currentUser = user;
                        this.showComments = !this.showComments;
                    }
                });
            return;
        }

        this.showComments = !this.showComments;
    }

    public likePost(isLike: boolean) {
        if (!this.currentUser) {
            this.catchErrorWrapper(this.authService.getUser())
                .pipe(
                    switchMap((userResp) => this.reactionsService.reactToPost(this.post, userResp, isLike)),
                    takeUntil(this.unsubscribe$)
                )
                .subscribe((post) => (this.post = post));

            return;
        }

        this.reactionsService
            .reactToPost(this.post, this.currentUser, isLike)
            .pipe(takeUntil(this.unsubscribe$))
            .subscribe((post) => (this.post = post));
    }

    public sendComment() {
        this.newComment.authorId = this.currentUser.id;
        this.newComment.postId = this.post.id;

        this.commentService
            .createComment(this.newComment)
            .pipe(takeUntil(this.unsubscribe$))
            .subscribe(
                (resp) => {
                    if (resp) {
                        this.post.comments = this.sortCommentArray(this.post.comments.concat(resp.body));
                        this.newComment.body = undefined;
                    }
                },
                (error) => this.snackBarService.showErrorMessage(error)
            );
    }

    public openAuthDialog() {
        this.authDialogService.openAuthDialog(DialogType.SignIn);
    }

    public openUpdatePostDialog(post: Post) {
        this.postDialogsService.openUpdatePostDialog(post)
            .pipe(takeUntil(this.unsubscribe$))
            .subscribe(
                (newPost: UpdatePost) => {
                    if (newPost) {
                        post.body = newPost.body;
                        post.previewImage = newPost.previewImage;
                        post.isUpdated = true;
                    }
                }
            )
    }

    public openDeletePostDialog(post: Post) {
        this.postDialogsService.openDeletePostDialog(post)
            .pipe(takeUntil(this.unsubscribe$))
            .subscribe((isDeleted: Boolean) => {
                if (isDeleted) {
                    this.onDelete.next(post.id);
                }
            })
    }

    public openReactionsDialog(post: Post) {
        this.postDialogsService.openReactionsDialog(post);
    }

    private catchErrorWrapper(obs: Observable<User>) {
        return obs.pipe(
            catchError(() => {
                this.openAuthDialog();
                return empty();
            })
        );
    }

    private sortCommentArray(array: Comment[]): Comment[] {
        return array.sort((a, b) => +new Date(b.createdAt) - +new Date(a.createdAt));
    }
}
