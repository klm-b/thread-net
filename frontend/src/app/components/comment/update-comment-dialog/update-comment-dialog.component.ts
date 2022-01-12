import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Subject } from 'rxjs';
import { switchMap, takeUntil } from 'rxjs/operators';
import { UpdateComment } from 'src/app/models/comment/update-comment';
import { Comment } from 'src/app/models/comment/comment';
import { PostService } from 'src/app/services/post.service';
import { SnackBarService } from 'src/app/services/snack-bar.service';
import { CommentService } from 'src/app/services/comment.service';

@Component({
    templateUrl: './update-comment-dialog.component.html',
    styleUrls: ['./update-comment-dialog.component.sass']
})
export class UpdateCommentDialogComponent implements OnInit, OnDestroy {
    public comment = {} as UpdateComment;
    public loading = false;

    private unsubscribe$ = new Subject<void>();

    constructor(
        private dialogRef: MatDialogRef<UpdateCommentDialogComponent>,
        @Inject(MAT_DIALOG_DATA) public data: { comment: Comment },
        private commentService: CommentService,
        private snackBarService: SnackBarService
    ) {}

    public ngOnInit() {
        this.comment = {
            id: this.data.comment.id,
            body: this.data.comment.body,
        }
    }

    public updateComment() {
        this.loading = true;
        this.commentService.updateComment(this.comment)
            .pipe(takeUntil(this.unsubscribe$)).subscribe(
                () => {
                    this.snackBarService.showUsualMessage('Successfully updated');
                    this.loading = false;
                    this.dialogRef.close(this.comment);
                },
                (error) => {
                    this.snackBarService.showErrorMessage(error || "An error occurred while updating")
                    this.loading = false;
                    this.dialogRef.close();
                }
            );
    }

    public ngOnDestroy() {
        this.unsubscribe$.next();
        this.unsubscribe$.complete();
    }
}
