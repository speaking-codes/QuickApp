export class InsuranceCoverageSummary {
    $$index: number;
    customerCode: string;
    code: string;
    categoryType: string;
    itemDescription: string;
    issueDate: string;
    expiryDate: string;
    totalPrice: string;
}

export class InsuranceCategoryPolicyCard {
    code: string;
    name: string;
    abstract: string;
    iconCssClass: string;
    salesLineCode: string;
    salesLineName: string;
    salesLineBackgroundColor: string;
    salesLineBackgroundCssClass: string;
}

export class InsuranceCoveragePolicyFooter {
    code: string;
    name: string;
    iconCssClass: string;
    salesLineBackgroundCssClass: string;

    constructor(code: string, name: string, iconCssClass: string, salesLineBackgroundCssClass: string){
        this.code = code;
        this.name = name;
        this.iconCssClass = iconCssClass;
        this.salesLineBackgroundCssClass = salesLineBackgroundCssClass;
    }
}