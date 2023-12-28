export interface IAbout {
    name: string;
    logo: string;
    logoDescription: string;
    address: string;
    city: string;
    postalCode: string;
    country: string;
    phoneNumber: string;
    faxNumber: string;
    email: string;
    description1: string;
    description2: string;
    description3: string;
    description4: string;
    description5: string;
    description6: string;
    socialCapital: string;
}

export class About implements IAbout{
    public name: string;
    logo: string;
    logoDescription: string;
    address: string;
    city: string;
    postalCode: string;
    country: string;
    phoneNumber: string;
    faxNumber: string;
    email: string;
    public description1: string;
    description2: string;
    description3: string;
    description4: string;
    description5: string;
    description6: string;
    socialCapital: string;
}
