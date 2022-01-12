import { Component, Input } from '@angular/core';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { UpdateComment } from 'src/app/models/comment/update-comment';
import { User } from 'src/app/models/user';
import { CommentDialogsService } from 'src/app/services/comment-dialogs.service';
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

    public constructor(private commentDialogsService: CommentDialogsService) {}

    public openUpdatePostDialog(comment: Comment) {
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
}
