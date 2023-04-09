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
        lng: string,
        lat: string,
        status: number,
        businessId: number,
        userId: number,
        propertyTypeId: number,
        createdAt: DateTime,
        updatedAt: DateTime
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
    lng: string,
    lat: string,
    businessId: number,
    propertyTypeId: number
}

export type UpdatePropertyDto = {
    id: number,
    name: string,
    shortDesc: string,
    description: string,
    address: string,
    country: string,
    city: string,
    lng: string,
    lat: string,
    businessId: number,
    propertyTypeId: number
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