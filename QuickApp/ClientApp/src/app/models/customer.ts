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

}

export class CustomerDetailHeader extends Customer{
    fullName: string;
    residence: string;
    phoneNumber: string;
    email: string;
}