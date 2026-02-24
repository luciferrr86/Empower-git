
import { Injectable } from '@angular/core';
import { HttpResponseBase } from '@angular/common/http';
import { Subject } from 'rxjs/Subject';
import { ToastyService, ToastyConfig, ToastyComponent, ToastOptions, ToastData } from 'ng2-toasty';
import { Utilities } from './utilities';
import { SweetAlertService } from 'ngx-sweetalert2';

@Injectable()
export class AlertService {
  position = 'top-right';
  title: string;
  msg: string;
  showClose = true;
  timeout = 5000;
  theme = 'bootstrap';
  type = 'default';
  closeOther = false;
  constructor(private sweetAlert: SweetAlertService, private toastyService: ToastyService) { }

  private messages = new Subject<AlertMessage>();
  private stickyMessages = new Subject<AlertMessage>();
  private dialogs = new Subject<AlertDialog>();

  private _isLoading = false;
  private loadingMessageId: any;



  showDialog(message: string)
  showDialog(message: string, type: DialogType, okCallback: (val?: any) => any)
  showDialog(message: string, type: DialogType, okCallback?: (val?: any) => any, cancelCallback?: () => any, okLabel?: string, cancelLabel?: string, defaultValue?: string)
  showDialog(message: string, type?: DialogType, okCallback?: (val?: any) => any, cancelCallback?: () => any, okLabel?: string, cancelLabel?: string, defaultValue?: string) {
    if (!type)
      type = DialogType.alert;

    this.dialogs.next({ message: message, type: type, okCallback: okCallback, cancelCallback: cancelCallback, okLabel: okLabel, cancelLabel: cancelLabel, defaultValue: defaultValue });
  }



  showMessage(summary: string)
  showMessage(summary: string, detail: string, severity: MessageSeverity)
  showMessage(summaryAndDetails: string[], summaryAndDetailsSeparator: string, severity: MessageSeverity)
  showMessage(response: HttpResponseBase, ignoreValue_useNull: string, severity: MessageSeverity)
  showMessage(data: any, separatorOrDetail?: string, severity?: MessageSeverity) {
    if (!severity)
      severity = MessageSeverity.default;

    if (data instanceof HttpResponseBase) {
      data = Utilities.getHttpResponseMessage(data);
      separatorOrDetail = Utilities.captionAndMessageSeparator;
    }

    if (data instanceof Array) {
      for (let message of data) {
        let msgObject = Utilities.splitInTwo(message, separatorOrDetail);

        this.showMessageHelper(msgObject.firstPart, msgObject.secondPart, severity, false);
      }
    }
    else {
      this.showMessageHelper(data, separatorOrDetail, severity, false);
    }
  }


  showStickyMessage(summary: string)
  showStickyMessage(summary: string, detail: string, severity: MessageSeverity, error?: any)
  showStickyMessage(summaryAndDetails: string[], summaryAndDetailsSeparator: string, severity: MessageSeverity)
  showStickyMessage(response: HttpResponseBase, ignoreValue_useNull: string, severity: MessageSeverity)
  showStickyMessage(data: string | string[] | HttpResponseBase, separatorOrDetail?: string, severity?: MessageSeverity, error?: any) {
    if (!severity)
      severity = MessageSeverity.default;

    if (data instanceof HttpResponseBase) {
      data = Utilities.getHttpResponseMessage(data);
      separatorOrDetail = Utilities.captionAndMessageSeparator;
    }


    if (data instanceof Array) {
      for (let message of data) {
        let msgObject = Utilities.splitInTwo(message, separatorOrDetail);

        this.showMessageHelper(msgObject.firstPart, msgObject.secondPart, severity, true);
      }
    }
    else {

      if (error) {

        let msg = `Severity: "${MessageSeverity[severity]}", Summary: "${data}", Detail: "${separatorOrDetail}", Error: "${Utilities.safeStringify(error)}"`;

        switch (severity) {
          case MessageSeverity.default:
            this.logInfo(msg);
            break;
          case MessageSeverity.info:
            this.logInfo(msg);
            break;
          case MessageSeverity.success:
            this.logMessage(msg);
            break;
          case MessageSeverity.error:
            this.logError(msg);
            break;
          case MessageSeverity.warn:
            this.logWarning(msg);
            break;
          case MessageSeverity.wait:
            this.logTrace(msg);
            break;
        }
      }

      this.showMessageHelper(data, separatorOrDetail, severity, true);
    }
  }



  private showMessageHelper(summary: string, detail: string, severity: MessageSeverity, isSticky: boolean) {
    this.showToast({ title: summary, msg: detail, type: severity });
  }



  startLoadingMessage(message = "Loading...", caption = "") {

    this._isLoading = true;
    clearTimeout(this.loadingMessageId);
    this.loadingMessageId = setTimeout(() => {
      this.showStickyMessage(caption, message, MessageSeverity.wait);
    }, 1000);
  }

  stopLoadingMessage() {
    this._isLoading = false;
    clearTimeout(this.loadingMessageId);
    this.resetStickyMessage();
  }



  logDebug(msg) {
  }

  logError(msg) {
  }

  logInfo(msg) {
  }

  logMessage(msg) {
  }

  logTrace(msg) {
  }

  logWarning(msg) {
  }

  resetStickyMessage() {
    this.stickyMessages.next();
  }



  showToast(options) {
    if (options.closeOther) {
      this.toastyService.clearAll();
    }
    this.position = "top-right"
    const toastOptions: ToastOptions = {
      title: options.title,
      msg: options.msg,
      showClose: true,
      timeout: 5000,
      theme: 'bootstrap',
      onAdd: (toast: ToastData) => {
        /* added */
      },
      onRemove: (toast: ToastData) => {
        /* removed */
      }
    };

    switch (options.type) {
      case MessageSeverity.default: this.toastyService.default(toastOptions); break;
      case MessageSeverity.info: this.toastyService.info(toastOptions); break;
      case MessageSeverity.success: this.toastyService.success(toastOptions); break;
      case MessageSeverity.wait: this.toastyService.wait(toastOptions); break;
      case MessageSeverity.error: this.toastyService.error(toastOptions); break;
      case MessageSeverity.warn: this.toastyService.warning(toastOptions); break;
    }
  }

  /**Sweet Alert */
  showConfirm(message: string, okCallback: (val?: any) => any) {
    this.sweetAlert.confirm({

      title: message,
      text: "You will not be able to recover this!",
      type: "warning",
      showCancelButton: true,
      confirmButtonColor: "#DD6B55",
      confirmButtonText: "Yes, delete it!",
      closeOnConfirm: false
    })
      .then(() => {
        okCallback();
      })
      .catch(() => console.log('canceled'));

  }
  showConfirmCancel(message: string, buttonText: string, titleSucess: string, okCallback: (val?: any) => any) {
    this.sweetAlert.confirm({

      title: message,
      text: "You will not be able to recover this!",
      type: "warning",
      showCancelButton: true,
      confirmButtonColor: "#DD6B55",
      confirmButtonText: buttonText,
      closeOnConfirm: false
    })
      .then(() => {
        okCallback();
        this.sweetAlert.success({
          title: titleSucess
        });
      })
      .catch(() => console.log('canceled'));

  }
  showConfirmCancelMessage(message: string, buttonText: string, okCallback: (val?: any) => any) {
    this.sweetAlert.confirm({

      title: message,
      text: "",
      type: "warning",
      showCancelButton: true,
      confirmButtonColor: "#DD6B55",
      confirmButtonText: buttonText,
      closeOnConfirm: false
    })
      
      .then(() => {
        okCallback();
      })
      .catch(() => console.log('canceled'));

  }
  showInfoMessage(message: string) {
    this.sweetAlert.swal({
      title: message,
      type: 'info'
    });
  }
  showSucessMessage(message: string) {
    this.sweetAlert.swal({
      title: message,
      type: 'success'
    });
  }


  get isLoadingInProgress(): boolean {
    return this._isLoading;
  }
}







//******************** Dialog ********************//
export class AlertDialog {
  constructor(public message: string, public type: DialogType, public okCallback: (val?: any) => any, public cancelCallback: () => any,
    public defaultValue: string, public okLabel: string, public cancelLabel: string) {

  }
}

export enum DialogType {
  alert,
  confirm,
  prompt
}
//******************** End ********************//






//******************** Growls ********************//
export class AlertMessage {
  constructor(public severity: MessageSeverity, public summary: string, public detail: string) { }
}

export enum MessageSeverity {
  default,
  info,
  success,
  error,
  warn,
  wait
}
//******************** End ********************//
