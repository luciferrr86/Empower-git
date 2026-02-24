import { PermissionValues } from './permission.model';

export interface LoginResponse {
    access_token: string;
    id_token: string;
    refresh_token: string;
    expires_in: number;
}
export interface IdToken {
    sub: string;
    name: string;
    fullname: string;
    email: string;
    phone: string;
    role: string | string[];
    permission: PermissionValues | PermissionValues[];
    configuration: string;
    type: string;
    leave: string;
    recruitment: string;
    timesheet: string;
    performance: string;
    salesMarketing: string;
    expanseManagement: string;
}
