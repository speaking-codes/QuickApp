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

export class CustomerHeader extends Customer{
    fullName: string;
    address: string;
    phone: string;
    email: string;
}

export class CustomerDetail extends Customer{
    fullName: string;
    gender: string;
    birthDate: string;
    birthPlace: string;
    maritalStatus: string;
    childrenNumber: string;
    addressLocation: string;
    addressCity: string;
    phone: string;
    email: string;
    contractType: string;
    jobTitle: string;
    income: string;
}