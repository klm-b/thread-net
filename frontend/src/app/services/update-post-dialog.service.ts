import { Injectable, OnDestroy } from '@angular/core';
import { DialogType } from '../models/common/auth-dialog-type';
import { User } from '../models/user';
import { MatDialog } from '@angular/material/dialog';
import { map, takeUntil } from 'rxjs/operators';
import { AuthenticationService } from './auth.service';
import { Subscription, Subject } from 'rxjs';
import { UpdatePostDialogComponent } from '../components/update-post-dialog/update-post-dialog.component';
import { SnackBarService } from './snack-bar.service';
import { Post } from '../models/post/post';
import { UpdatePost } from '../models/post/update-post';

@Injectable({ providedIn: 'root' })
export class UpdatePostDialogService implements OnDestroy {
    private unsubscribe$ = new Subject<void>();

    public constructor(private dialog: MatDialog, private authService: AuthenticationService, private snackBarService: SnackBarService,) { }

    public openUpdatePostDialog(post: Post) {
        const dialog = this.dialog.open(UpdatePostDialogComponent, {
            data: { post: post },
            autoFocus: true,
            disableClose: true,
            width: '50vw',
            backdropClass: 'dialog-backdrop'
        });

        dialog
            .afterClosed()
            .pipe(takeUntil(this.unsubscribe$))
            .subscribe(
                (newPost: UpdatePost) => {
                    if (newPost) {
                        post.body = newPost.body;
                        post.previewImage = newPost.previewImage;
                        post.isUpdated = true;
                    }
                }
            );
    }

    public ngOnDestroy() {
        this.unsubscribe$.next();
        this.unsubscribe$.complete();
    }
}
