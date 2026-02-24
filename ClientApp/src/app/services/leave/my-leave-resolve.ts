import { Injectable } from '@angular/core';
import { Router, Resolve, RouterStateSnapshot, ActivatedRouteSnapshot, ActivatedRoute } from '@angular/router';
import { MyLeaveService } from './my-leave.service';
import { MyLeave } from '../../models/leave/leave-myleave.model';
import { AccountService } from '../account/account.service';

@Injectable()
export class MyLeaveResolver implements Resolve<MyLeave> {
    constructor(private myLeaveService: MyLeaveService, private accountService: AccountService, private routers: Router) {

    }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Promise<MyLeave> {
        return this.myLeaveService.creatEntitlement(this.accountService.currentUser.id).then((data) => {
            if (!data.isSet) {
                this.routers.navigate(['/leave/check-config']);
            } else {
                return data;
            }
        }).catch(this.handleError);

    }
    public extractData(res: MyLeave) {
        if (!res.isSet) {
            this.routers.navigate(['/leave/check-config']);
        } else {
            return res;
        }
    }

    public handleError(error: any): Promise<any> {
        return Promise.reject(error.message || error);
    }
}