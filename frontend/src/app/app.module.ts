import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { JwtInterceptor } from './helpers/jwt.interceptor';
import { ErrorInterceptor } from './helpers/error.interceptor';
import { RouterModule } from '@angular/router';
import { AppRoutes } from './app.routes';
import { FormsModule } from '@angular/forms';
import { MainThreadComponent } from './components/main-thread/main-thread.component';
import { PostComponent } from './components/post/post.component';
import { HomeComponent } from './components/home/home.component';
import { UserProfileComponent } from './components/user-profile/user-profile.component';
import { AuthDialogComponent } from './components/auth-dialog/auth-dialog.component';
import { CommentComponent } from './components/comment/comment.component';
import { MaterialComponentsModule } from './components/common/material-components.module';
import { UpdatePostDialogComponent } from './components/post/update-post-dialog/update-post-dialog.component';
import { DeletePostDialogComponent } from './components/post/delete-post-dialog/delete-post-dialog.component';
import { ReactionsDialogComponent } from './components/post/reactions-dialog/reactions-dialog.component';
import { UpdateCommentDialogComponent } from './components/comment/update-comment-dialog/update-comment-dialog.component';
import { DeleteCommentDialogComponent } from './components/comment/delete-comment-dialog/delete-comment-dialog.component';
import { CommentReactionsDialogComponent } from './components/comment/comment-reactions-dialog/comment-reactions-dialog.component';

@NgModule({
    declarations: [AppComponent, MainThreadComponent, PostComponent, HomeComponent, UserProfileComponent, AuthDialogComponent, CommentComponent, UpdatePostDialogComponent, DeletePostDialogComponent, ReactionsDialogComponent, UpdateCommentDialogComponent, DeleteCommentDialogComponent, CommentReactionsDialogComponent],
    imports: [BrowserModule, BrowserAnimationsModule, HttpClientModule, MaterialComponentsModule, RouterModule.forRoot(AppRoutes), FormsModule],
    exports: [MaterialComponentsModule],
    providers: [
        { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
        { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true }
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
