export class InsuranceCoverageSummary {
    code: string;
    categoryType: string;
    itemDescription: string;
    issueDate: string;
    expiryDate: string;
    totalPrice: string;
    constructor(code: string, categoryType: string, itemDescription: string, issueDate: string, exipiryDate: string, totalPrice: string){
        this.code = code;
        this.categoryType = categoryType;
        this.itemDescription = itemDescription;
        this.issueDate = issueDate;
        this.expiryDate = exipiryDate;
        this.totalPrice = totalPrice;
    }
}
