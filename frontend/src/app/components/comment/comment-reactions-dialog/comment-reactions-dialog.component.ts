import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { Comment } from 'src/app/models/comment/comment';
import { Reaction } from 'src/app/models/reactions/reaction';
import { CommentService } from 'src/app/services/comment.service';
import { PostService } from 'src/app/services/post.service';
import { SnackBarService } from 'src/app/services/snack-bar.service';

@Component({
    templateUrl: './comment-reactions-dialog.component.html',
    styleUrls: ['./comment-reactions-dialog.component.sass']
})
export class CommentReactionsDialogComponent implements OnInit, OnDestroy {

    public reactions: Reaction[] = [];
    public cachedReactions: Reaction[] = [];
    public loading = false;
    public reactionsFilter?: string = 'all';

    private unsubscribe$ = new Subject<void>();

    constructor(
        private dialogRef: MatDialogRef<CommentReactionsDialogComponent>,
        @Inject(MAT_DIALOG_DATA) public data: { comment: Comment },
        private commentService: CommentService,
        private snackBarService: SnackBarService
    ) {}

    ngOnInit(): void {
        this.loading = true;
        this.commentService
            .getReactionsToComment(this.data.comment.id)
            .pipe(takeUntil(this.unsubscribe$))
            .subscribe(
                (resp) => {
                    this.loading = false;
                    this.reactions = this.cachedReactions = resp.body;
                },
                (error) => {
                    this.snackBarService.showErrorMessage(error || "An error occurred while loading")
                    this.loading = false;
                    this.dialogRef.close();
                }
            );
    }

    onFilterChange(value: string) {
        switch (value) {
            case 'likes':
                this.reactions = this.cachedReactions.filter(r => r.isLike)
                break;

            case 'dislikes':
                this.reactions = this.cachedReactions.filter(r => r.isLike === false)
                break;

            case 'all':
                this.reactions = this.cachedReactions;
                break;
        }
    }

    public ngOnDestroy() {
        this.unsubscribe$.next();
        this.unsubscribe$.complete();
    }
}
