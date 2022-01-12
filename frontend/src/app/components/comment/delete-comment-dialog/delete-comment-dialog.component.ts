import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { Comment } from 'src/app/models/comment/comment';
import { CommentService } from 'src/app/services/comment.service';
import { PostService } from 'src/app/services/post.service';
import { SnackBarService } from 'src/app/services/snack-bar.service';
import { DeletePostDialogComponent } from '../../post/delete-post-dialog/delete-post-dialog.component';

@Component({
    templateUrl: './delete-comment-dialog.component.html',
    styleUrls: ['./delete-comment-dialog.component.sass']
})
export class DeleteCommentDialogComponent implements OnInit {
    public commentId: number;
    public loading = false;

    private unsubscribe$ = new Subject<void>();

    constructor(
        private dialogRef: MatDialogRef<DeletePostDialogComponent>,
        @Inject(MAT_DIALOG_DATA) public data: { comment: Comment },
        private commentService: CommentService,
        private snackBarService: SnackBarService
    ) {}

    ngOnInit(): void {
        this.commentId = this.data.comment.id;
    }

    public deleteComment() {
        this.loading = true;

        this.commentService
            .deleteComment(this.commentId)
            .pipe(takeUntil(this.unsubscribe$)).subscribe(
                () => {
                    this.snackBarService.showUsualMessage('Successfully deleted');
                    this.loading = false;
                    this.dialogRef.close(true);
                },
                (error) => {
                    this.snackBarService.showErrorMessage(error || "An error occurred while deleting")
                    this.loading = false;
                    this.dialogRef.close();
                }
            );
    }
}
