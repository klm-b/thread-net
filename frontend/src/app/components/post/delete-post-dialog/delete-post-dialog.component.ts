import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { Post } from 'src/app/models/post/post';
import { PostService } from 'src/app/services/post.service';
import { SnackBarService } from 'src/app/services/snack-bar.service';

@Component({
    templateUrl: './delete-post-dialog.component.html',
    styleUrls: ['./delete-post-dialog.component.sass']
})
export class DeletePostDialogComponent implements OnInit {
    public postId: number;
    public loading = false;

    private unsubscribe$ = new Subject<void>();

    constructor(
        private dialogRef: MatDialogRef<DeletePostDialogComponent>,
        @Inject(MAT_DIALOG_DATA) public data: { post: Post },
        private postService: PostService,
        private snackBarService: SnackBarService
    ) {}

    ngOnInit(): void {
        this.postId = this.data.post.id;
    }

    public deletePost() {
        this.loading = true;

        this.postService
            .deletePost(this.postId)
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
