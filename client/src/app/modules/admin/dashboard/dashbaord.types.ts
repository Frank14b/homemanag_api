export type TotalData = {
    business : {
        active: number,
        inactive: number,
    },
    properties: {
        active: number,
        inactive: number,
    },
    users: {
        employees: number,
        all: number,
    },
    maintenances: {
        completed: number,
        pending: number,
        inprogress: number,
    }
}