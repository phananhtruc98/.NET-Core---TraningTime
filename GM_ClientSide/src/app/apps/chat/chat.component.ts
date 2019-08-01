import { Component, OnDestroy, OnInit, ViewEncapsulation } from "@angular/core";
import { Subject } from "rxjs";
import { takeUntil } from "rxjs/operators";

import { fuseAnimations } from "@fuse/animations";

import { ChatService } from "./chat.service";
import { HubConnection } from "@aspnet/signalr";
import { ProfileHubService } from "app/core/data-api/hubs/profile.hub";
import { NotificationMiddlewareService } from 'app/core/notification-middleware.service';

@Component({
    selector: "chat",
    templateUrl: "./chat.component.html",
    styleUrls: ["./chat.component.scss"],
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations
})
export class ChatComponent implements OnInit, OnDestroy {
    selectedChat: any = null;

    // Private
    private _unsubscribeAll: Subject<any>;
    private _hubConnection: HubConnection | undefined;


    /**
     * Constructor
     *
     * @param {ChatService} _chatService
     */
    constructor(
        private _chatService: ChatService,
        private _profileHub: ProfileHubService,
        public notificationMiddleware: NotificationMiddlewareService
    ) {
        // Set the private defaults
        this._unsubscribeAll = new Subject();
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Lifecycle hooks
    // -----------------------------------------------------------------------------------------------------

    /**
     * On init
     */
    ngOnInit(): void {

        this.selectedChat = null;
        this._chatService.onChatSelected
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe(chatData => {
                this.selectedChat = chatData;
            });
    }

    /**
     * On destroy
     */
    ngOnDestroy(): void {
        // Unsubscribe from all subscriptions
        this._unsubscribeAll.next();
        this._unsubscribeAll.complete();
    }
    toggleSubscription() {
        
        this.notificationMiddleware.toggleSubscription();
    }

    cleanUrl(url) {
        if (url.indexOf(self.location.origin) >= 0) {
            return url.replace(self.location.origin, '');
        }
        return url;
    }

    removeNotif(notif) {
        var index = this.notificationMiddleware.notifications.indexOf(notif);
        if (index >= 0) {
            this.notificationMiddleware.notifications.splice(index, 1);
        }
    }
}