import { DropDownList } from "../../common/dropdown";

export class ReviewRating {
    public rating:Ratings[];
    public managerComment:string;
    public managerSignature:string;
    public employeeComment:string;

}

export class Ratings{
    public ratingId:string;
public ratingName:string;
}