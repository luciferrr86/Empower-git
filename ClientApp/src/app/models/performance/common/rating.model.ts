import { CheckSaveSubmit } from "./check-save-submit.model";

export class RatingModel {
    constructor() {
        this.checkSaveSubmit = new CheckSaveSubmit();
        this.ratingList = new Array<Ratings>();
    }
    public instructionText: string;
    public ratingList: Ratings[];
    public midYearRating: Rating;
    public endYearRating: Rating;
    public checkSaveSubmit: CheckSaveSubmit;
}
export class Rating {
    public ratingId: string;
    public managerComment: string;
    public managerSignature: string;
    public employeeComment: string;

}

export class Ratings {
    public ratingId: string;
    public ratingName: string;
    public ratingDescription: string;
}