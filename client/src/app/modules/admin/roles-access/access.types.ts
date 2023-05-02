import { DateTime } from "luxon"

export type ResultAccessListDto = [
    {
        id: number,
        userName: string,
        firstName: string,
        lastName: string,
        email: string
    }
]

export type ResultAccessDto = {
    data: ResultAccessListDto,
    total: number,
    skip: number,
    limit: number
    sort: string
}

export type ResultDeleteAcces = {
    status: boolean,
    message: string
}

export type CreateAccesDto = {
    name: string,
    shortDesc: string,
    description: string,
    address: string,
    country: string,
    city: string,
    lng: number,
    lat: number,
    businessId: number,
    propertyTypeId: number,
    typeMode: number,
    dataLocation: string
}

export type UpdateAccesDto = {
    id: number,
    name: string,
    shortDesc: string,
    description: string,
    address: string,
    country: string,
    city: string,
    lng: number,
    lat: number,
    businessId: number,
    propertyTypeId: number,
    typeMode: number,
    dataLocation: string
}