import { Permission } from './permission.model';
export class Role {

    constructor(name?: string, description?: string, permissions?: Permission[]) {

        this.name = name;
        this.description = description;
        this.permissions = permissions;
    }

    public id: string;
    public name: string;
    public description: string;
    public usersCount: string;
    public permissions: Permission[];
}
export class RoleModel {

    constructor(roleModel?: Role[], totalCount?: number) {
       this.roleModel= new Array<Role>();
        this.totalCount = totalCount;
       
    }
    public totalCount: number;
    public roleModel: Role[];
}
