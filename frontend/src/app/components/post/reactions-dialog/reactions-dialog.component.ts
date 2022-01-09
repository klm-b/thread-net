import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { Post } from 'src/app/models/post/post';
import { Reaction } from 'src/app/models/reactions/reaction';
import { PostService } from 'src/app/services/post.service';
import { ReactionService } from 'src/app/services/reaction.service';
import { SnackBarService } from 'src/app/services/snack-bar.service';

@Component({
    templateUrl: './reactions-dialog.component.html',
    styleUrls: ['./reactions-dialog.component.sass']
})
export class ReactionsDialogComponent implements OnInit, OnDestroy {

    public reactions: Reaction[] = [];
    public cachedReactions: Reaction[] = [];
    public loading = false;
    public reactionsFilter?: string = 'all';

    private unsubscribe$ = new Subject<void>();

    constructor(
        private dialogRef: MatDialogRef<ReactionsDialogComponent>,
        @Inject(MAT_DIALOG_DATA) public data: { post: Post },
        private postService: PostService,
        private snackBarService: SnackBarService
    ) {}

    ngOnInit(): void {
        this.loading = true;
        this.postService
            .getReactionsToPost(this.data.post.id)
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
