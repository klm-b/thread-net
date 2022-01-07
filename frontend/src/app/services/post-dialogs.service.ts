import { Injectable, OnDestroy } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { AuthenticationService } from './auth.service';
import { Subject, Observable } from 'rxjs';
import { UpdatePostDialogComponent } from '../components/post/update-post-dialog/update-post-dialog.component';
import { SnackBarService } from './snack-bar.service';
import { Post } from '../models/post/post';
import { UpdatePost } from '../models/post/update-post';
import { DeletePostDialogComponent } from '../components/post/delete-post-dialog/delete-post-dialog.component';

@Injectable({ providedIn: 'root' })
export class PostDialogsService implements OnDestroy {
    private unsubscribe$ = new Subject<void>();

    public constructor(private dialog: MatDialog, private authService: AuthenticationService, private snackBarService: SnackBarService,) {}

    public openUpdatePostDialog(post: Post): Observable<UpdatePost> {
        const dialog = this.dialog.open(UpdatePostDialogComponent, {
            data: { post: post },
            autoFocus: true,
            disableClose: true,
            width: '50vw',
            backdropClass: 'dialog-backdrop'
        });

        return dialog.afterClosed();
    }

    public openDeletePostDialog(post: Post): Observable<boolean> {
        const dialog = this.dialog.open(DeletePostDialogComponent, {
            data: { post: post },
            autoFocus: false,
            backdropClass: 'dialog-backdrop'
        });

        return dialog.afterClosed();
    }

    public ngOnDestroy() {
        this.unsubscribe$.next();
        this.unsubscribe$.complete();
    }
}
