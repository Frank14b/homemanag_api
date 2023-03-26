import { DateTime } from "luxon"

export type ResultTypeDto = [
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

export type ResultListDto = {
    data: ResultTypeDto,
    total: number,
    skip: number,
    limit: number
}

export type ResultDeleteDto = {
    status: boolean,
    message: string
}

export type CreateTypesDto = {
    name: string,
    description: string,
    subTypeId: number,
}

export type UpdateTypesDto = {
    id: number
    name: string,
    description: string,
    subTypeId: number,
}