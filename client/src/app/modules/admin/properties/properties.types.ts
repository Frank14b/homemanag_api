import { DateTime } from "luxon"

export type ResultPropertiesListDto = [
    {
        id: number,
        name: string,
        reference: string,
        shortDesc: string,
        description: string,
        address: string,
        country: string,
        city: string,
        lng: number,
        lat: number,
        status: number,
        businessId: number,
        userId: number,
        propertyTypeId: number,
        createdAt: DateTime,
        updatedAt: DateTime,
        typeMode: number,
        dataLocation: string
    }
]

export type ResultPropertiesDto = {
    data: ResultPropertiesListDto,
    total: number,
    skip: number,
    limit: number
    sort: string
}

export type ResultDeleteProperties = {
    status: boolean,
    message: string
}

export type CreatePropertyDto = {
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

export type UpdatePropertyDto = {
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

export type DataLocation = {
    name: string,
    lat: number,
    lng: number,
    url: string,
    city: string,
    country: string,
    countryCode: string
}