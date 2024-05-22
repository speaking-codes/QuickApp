export class InsuranceCoverageSummary {
    $$index: number;
    customerCode: string;
    code: string;
    categoryType: string;
    itemDescription: string[];
    issueDate: string;
    expiryDate: string;
    totalPrice: string;
    isTopSelling: boolean;
    warrantySelecteds: string[];
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

export class InsuranceCoverageSalesLineChart {
    salesLineCode: string;
    salesLineName: string;
    backGroundColor: string;
    totalPrice: number;
    totalCount: number;
}

export class ChartData {
    data: number[];
    label: string;
    fill: string;

    constructor(value: number, label: string, fill: string)
    {
        this.data = [value];
        this.label = label;
        this.fill = fill;
    }
}