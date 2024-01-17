import { EnumContractType, EnumGender } from "./enums";

export class Customer {
    customerCode: string;
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
    customerCode: string;
    firstName: string;
    lastName: string;
    birthDate: string;
    birthPlace: string;
    birthCounty: string;
    gender: EnumGender;
    profession: string;
    contractType: EnumContractType;
    ral: number;  

    location: string;
    city: string;
    postalCode: string;
    province: string;
        
    email: string;
    phoneNumber: string;
}

export class CustomerDetailHeader extends Customer{
    fullName: string;
    residence: string;
    phoneNumber: string;
    email: string;
}