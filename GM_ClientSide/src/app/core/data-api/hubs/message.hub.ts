import { Injectable } from "@angular/core";
import * as signalR from "@aspnet/signalr";
import { AuthService } from "app/core/auth/auth.service";
import { MessageModel } from "./../../../models/message.model";
import { BehaviorSubject } from "rxjs";
import { environment } from "environments/environment";
import { UnreadMessage } from "app/models/UnreadMessage.model";
import { NotificationModel, NotificationService } from 'app/core/generated';
import { NotificationMiddlewareService } from 'app/core/notification-middleware.service';

@Injectable({
    providedIn: "root"
})
export class MessageHubService {

    newChatMessage: BehaviorSubject<MessageModel>;
    removedChatMessage: BehaviorSubject<MessageModel>;
    _hubConnection: signalR.HubConnection;
    unreadMessage: BehaviorSubject<UnreadMessage>;
    public pushNotificationStatus = {
        isSubscribed: true,
        isSupported: false,
        isInProgress: false
    };
    constructor(private authService: AuthService
        , private notificationService: NotificationService,
        private _notificationMiddlewareService: NotificationMiddlewareService, private pushSubcription: PushSubscription) {

        this.newChatMessage = new BehaviorSubject(null);
        this.unreadMessage = new BehaviorSubject(null);
        this.startConnection();
        this._hubConnection.on("SendMessage",
            (message: MessageModel) => {
                this.newChatMessage.next(message);
            });
        this._hubConnection.on("SendRemovedMessage",
            (message: MessageModel) => {
                this.removedChatMessage.next(message);
            });
        this._hubConnection.on("SendUnreadMessagesAmount",
            (message: UnreadMessage) => {
                this.unreadMessage.next(message);
            });
        // this._hubConnection.on("PushNotification",
        //     (notification: NotificationModel) => {
        //         //
        //         this.pushNotificationStatus.isInProgress = true;
        //         const applicationServerKey = _notificationMiddlewareService.urlB64ToUint8Array(environment.applicationServerPublicKey);
        //         _notificationMiddlewareService.swRegistration.pushManager.subscribe({
        //             userVisibleOnly: true,
        //             applicationServerKey: applicationServerKey
        //         })
        //             .then(subscription => {
        //                 console.log(subscription);
        //                 console.log(JSON.stringify(subscription));
        //                 var newSub = JSON.parse(JSON.stringify(subscription));
        //                 console.log(newSub);

        //                 this.notificationService.broadcast(notification).subscribe(() => {
        //                 });
        //                 this.notificationService.subscribe(<PushSubscription>{
        //                     auth: newSub.keys.auth,
        //                     p256Dh: newSub.keys.p256dh,
        //                     endPoint: newSub.endpoint
        //                 }).subscribe(s => {
        //                     this.pushNotificationStatus.isSubscribed = true;
        //                 })
        //             })
        //             .catch(err => {
        //                 console.log('Failed to subscribe the user: ', err);
        //             })
        //             .then(() => {
        //                 this.pushNotificationStatus.isInProgress = false;
        //             });
        //         // _notificationMiddlewareService.subscribeUser();

        //         // this.unreadMessage.next(message);
        //     });
    }

    startConnection = () => {
        const securityToken = this.authService.getToken();
        this._hubConnection = new signalR.HubConnectionBuilder()
            .withUrl(environment.hub.messageUrl, { accessTokenFactory: () => securityToken })
            .build();
        this._hubConnection.serverTimeoutInMilliseconds = environment.hub.serverTimeoutInSeconds * 1000;
        this._hubConnection
            .start()
            .then(() => console.log("[Message Hub]: Connection started"))
            .catch(err => console.log(`[Message Hub]: Error while starting connection: ${err}`));
    };

    addSendMessageToUser(message: MessageModel, toUser: string) {
        this._hubConnection.invoke("SendMessageToUser", message, toUser).catch(function (err) {
            return console.error(err.toString());
        });
    }

    addSendRemovedMessageToUser(chatMessageModel: MessageModel, toUser: string) {
        this._hubConnection.invoke("SendRemovedMessageToUser", chatMessageModel, toUser).catch(function (err) {
            return console.error(err.toString());
        });
    }
}