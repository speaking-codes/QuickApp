import { EnumContractType, EnumGender } from "./enums";

export class Customer {
    taxIdCode: string;
}

export class CustomerGrid extends Customer{
    $$index: number;
    fullName: string;    
    birthDate: string;
    gender: string;
    residence: string;
    isActive: boolean;    
}    

export class CustomerEdit extends Customer {
    taxIdCode: string;
    firstName: string;
    lastName: string;
    birthDate: string;
    birthPlace: string;
    birthCounty: string;
    gender: EnumGender;
    profession: string;
    contractType: EnumContractType;
    ral: number;  
}

export class CustomerDetailHeader extends Customer{
    fullName: string;
    residence: string;
    phoneNumber: string;
    email: string;
}