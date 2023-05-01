import { DateTime } from "luxon"

export type ResultUsersListDto = [
    {
        id: number,
        userName: string,
        firstName: string,
        lastName: string,
        email: string
    }
]

export type ResultUsersDto = {
    data: ResultUsersListDto,
    total: number,
    skip: number,
    limit: number
    sort: string
}

export type ResultDeleteUser = {
    status: boolean,
    message: string
}

export type CreateUserDto = {
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

export type UpdateUserDto = {
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