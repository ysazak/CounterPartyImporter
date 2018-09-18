export class Company {
    public id: number;
    public externalId: string;
    public tradingName: string;
    public legalName: string;
    public companyType: CompanyType;
    public phone: number;
    public fax: number;
}

export enum CompanyType {
    None = 0,
    Buyer = 1,
    Seller = 2,
    BuyerAndSeller = 3
}

