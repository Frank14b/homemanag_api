import { DateTime } from "luxon"

export type ResultBusinessDto = [
    {
        id: number,
        subTypeId: number,
        name: string,
        description: string,
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
    subTypeId: number,
}

export type UpdateBusinessDto = {
    id: number
    name: string,
    description: string,
    subTypeId: number,
}