export class EducationlDetail {
    constructor(id?: string, hdPassingYear?: string, hdPercentage?: string, hdSpecialization?: string, higherDegreeInstitue?: string, highestQualification?: string
        , sdPassingYear?: string, sdPercentage?: string, sdSpecialization?: string, secondaryInstitute?: string, secondaryQualification?: string
        , sscInstitue?: string, sscPassingYear?: string, sscPercentage?: string, sscQualification?: string, sscSpecialization?: string) {

        this.id = id;
        this.hdPassingYear = hdPassingYear;
        this.hdPercentage = hdPercentage;
        this.hdSpecialization = hdSpecialization;
        this.higherDegreeInstitue = higherDegreeInstitue;
        this.highestQualification = highestQualification;
        this.sdPassingYear = sdPassingYear;
        this.sdPercentage = sdPercentage;
        this.sdSpecialization = sdSpecialization;
        this.secondaryInstitute = secondaryInstitute;
        this.secondaryQualification = secondaryQualification;
        this.sscInstitue = sscInstitue;
        this.sscPassingYear = sscPassingYear;
        this.sscPercentage = sscPercentage;
        this.sscQualification = sscQualification;
        this.sscSpecialization = sscSpecialization;
    }
    public id: string;
    public hdPassingYear: string;
    public hdPercentage: string;
    public hdSpecialization: string;
    public higherDegreeInstitue: string;
    public highestQualification: string;

    public sdPercentage: string;
    public sdSpecialization: string;
    public secondaryInstitute: string;
    public secondaryQualification: string;
    public sdPassingYear: string;

    public sscInstitue: string;
    public sscPassingYear: string;
    public sscPercentage: string;
    public sscQualification: string;
    public sscSpecialization: string;

}
