import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DialogType } from '../../../models/common/auth-dialog-type';
import { Subject } from 'rxjs';
import { AuthenticationService } from '../../../services/auth.service';
import { switchMap, takeUntil } from 'rxjs/operators';
import { SnackBarService } from '../../../services/snack-bar.service';
import { Post } from 'src/app/models/post/post';
import { UpdatePost } from 'src/app/models/post/update-post';
import { PostService } from 'src/app/services/post.service';
import { GyazoService } from 'src/app/services/gyazo.service';

@Component({
    templateUrl: './update-post-dialog.component.html',
    styleUrls: ['./update-post-dialog.component.sass']
})
export class UpdatePostDialogComponent implements OnInit, OnDestroy {
    public imageUrl: string;
    public imageFile: File;
    public post = {} as UpdatePost;
    public loading = false;

    private unsubscribe$ = new Subject<void>();

    constructor(
        private dialogRef: MatDialogRef<UpdatePostDialogComponent>,
        @Inject(MAT_DIALOG_DATA) public data: { post: Post },
        private postService: PostService,
        private gyazoService: GyazoService,
        private snackBarService: SnackBarService
    ) {}

    public ngOnInit() {

        this.post = {
            id: this.data.post.id,
            authorId: this.data.post.author.id,
            body: this.data.post.body,
            previewImage: this.data.post.previewImage
        }

        this.imageUrl = this.data.post.previewImage;
    }

    public loadImage(target: any) {
        this.imageFile = target.files[0];
        if (!this.imageFile) {
            target.value = '';
            return;
        }

        if (this.imageFile.size / 1000000 > 5) {
            target.value = '';
            this.snackBarService.showErrorMessage(`Image can't be heavier than ~5MB`);
            return;
        }

        const reader = new FileReader();
        reader.addEventListener('load', () => (this.imageUrl = reader.result as string));
        reader.readAsDataURL(this.imageFile);
    }

    public removeImage() {
        this.imageUrl = undefined;
        this.imageFile = undefined;
        this.post.previewImage = undefined;
    }

    public updatePost() {
        const postSubscription = !this.imageFile
            ? this.postService.updatePost(this.post)
            : this.gyazoService.uploadImage(this.imageFile).pipe(
                switchMap((imageData) => {
                    this.post.previewImage = imageData.url;
                    return this.postService.updatePost(this.post);
                })
            );

        this.loading = true;

        postSubscription.pipe(takeUntil(this.unsubscribe$)).subscribe(
            () => {
                this.snackBarService.showUsualMessage('Successfully updated');
                this.loading = false;
                this.dialogRef.close(this.post);
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
