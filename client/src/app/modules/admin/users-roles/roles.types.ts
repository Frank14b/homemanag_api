import { DateTime } from "luxon"

export type ResultRolesListDto = [
    {
        id: number,
        title: string,
        businessId: string,
        description: string,
        status: string,
        code: string,
        createdAt: DateTime
    }
]

export type ResultRolesDto = {
    data: ResultRolesListDto,
    total: number,
    skip: number,
    limit: number
    sort: string
}

export type ResultDeleteRole = {
    status: boolean,
    message: string
}

export type CreateRoleDto = {
    title: string,
    description: string,
    businessId: number,
}

export type UpdateRoleDto = {
    id: number,
    name: string,
    description: string,
    businessId: number,
}