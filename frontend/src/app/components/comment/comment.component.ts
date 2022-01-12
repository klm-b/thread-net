import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { UpdateComment } from 'src/app/models/comment/update-comment';
import { User } from 'src/app/models/user';
import { CommentDialogsService } from 'src/app/services/comment-dialogs.service';
import { ReactionService } from 'src/app/services/reaction.service';
import { Comment } from '../../models/comment/comment';

@Component({
    selector: 'app-comment',
    templateUrl: './comment.component.html',
    styleUrls: ['./comment.component.sass']
})
export class CommentComponent {
    @Input() public comment: Comment;
    @Input() public currentUser: User;

    private unsubscribe$ = new Subject<void>();

    @Output() onDelete = new EventEmitter<number>();

    public constructor(private commentDialogsService: CommentDialogsService, private reactionsService: ReactionService) {}

    public openUpdateCommentDialog(comment: Comment) {
        this.commentDialogsService.openUpdateCommentDialog(comment)
            .pipe(takeUntil(this.unsubscribe$))
            .subscribe(
                (newComment: UpdateComment) => {
                    if (newComment) {
                        this.comment.body = newComment.body;
                        this.comment.isUpdated = true;
                    }
                }
            )
    }

    public openDeleteCommentDialog(comment: Comment) {
        this.commentDialogsService.openDeleteCommentDialog(comment)
            .pipe(takeUntil(this.unsubscribe$))
            .subscribe((isDeleted: Boolean) => {
                if (isDeleted) {
                    this.onDelete.next(comment.id);
                }
            })
    }

    public reactToComment(isLike: boolean) {
        this.reactionsService
            .reactToComment(this.comment, this.currentUser, isLike)
            .pipe(takeUntil(this.unsubscribe$))
            .subscribe((post) => (this.comment = post));
    }
}
