import { DateTime } from "luxon"

export type ResultBusinessDto = [
    {
        id: number,
        name: string,
        description: string,
        address: string,
        country: string,
        city: string,
        countryCode: number,
        phoneNumber: number,
        email: string,
        lat: number,
        lng: number
        status: number,
        createdAt: DateTime,
        updatedAt: DateTime
    }
]

export type ResultBusinessListDto = {
    data: ResultBusinessDto,
    total: number,
    skip: number,
    limit: number
}

export type ResultDeleteDto = {
    status: boolean,
    message: string
}

export type CreateBusinessDto = {
    name: string,
    description: string,
    address: string,
    country: string,
    city: string,
    countryCode: number,
    phoneNumber: number,
    email: string,
    lat: number,
    lng: number
}

export type UpdateBusinessDto = {
    name: string,
    description: string,
    address: string,
    country: string,
    city: string,
    countryCode: number,
    phoneNumber: number,
    email: string,
    lat: number,
    lng: number
}