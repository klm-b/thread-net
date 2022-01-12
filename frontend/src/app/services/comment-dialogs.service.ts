import { Injectable, OnDestroy } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Subject, Observable } from 'rxjs';
import { SnackBarService } from './snack-bar.service';
import { Comment } from '../models/comment/comment';
import { UpdateCommentDialogComponent } from '../components/comment/update-comment-dialog/update-comment-dialog.component';
import { UpdateComment } from '../models/comment/update-comment';
import { DeleteCommentDialogComponent } from '../components/comment/delete-comment-dialog/delete-comment-dialog.component';
import { CommentReactionsDialogComponent } from '../components/comment/comment-reactions-dialog/comment-reactions-dialog.component';
import { takeUntil } from 'rxjs/operators';

@Injectable({ providedIn: 'root' })
export class CommentDialogsService implements OnDestroy {
    private unsubscribe$ = new Subject<void>();

    public constructor(private dialog: MatDialog, private snackBarService: SnackBarService) {}

    public openUpdateCommentDialog(comment: Comment): Observable<UpdateComment> {
        const dialog = this.dialog.open(UpdateCommentDialogComponent, {
            data: { comment: comment },
            autoFocus: true,
            disableClose: true,
            width: '50vw',
            backdropClass: 'dialog-backdrop'
        });

        return dialog.afterClosed();
    }

    public openDeleteCommentDialog(comment: Comment): Observable<boolean> {
        const dialog = this.dialog.open(DeleteCommentDialogComponent, {
            data: { comment: comment },
            autoFocus: false,
            backdropClass: 'dialog-backdrop'
        });

        return dialog.afterClosed();
    }

    public openReactionsDialog(comment: Comment) {
        const dialog = this.dialog.open(CommentReactionsDialogComponent, {
            data: { comment: comment },
            width: "500px",
            autoFocus: false,
            backdropClass: 'dialog-backdrop'
        });

        return dialog.afterClosed()
            .pipe(takeUntil(this.unsubscribe$))
            .subscribe();
    }

    public ngOnDestroy() {
        this.unsubscribe$.next();
        this.unsubscribe$.complete();
    }
}
