// 
const sortedDescCountries = (data: any) => {
    return data.sort((p1: any, p2: any) => (p1.name.common < p2.name.comm) ? 1 : (p1.name.common > p2.name.common) ? -1 : 0);
}

const sortedCountries = (data: any) => {
    return data.sort((p1: any, p2: any) => (p1.name.common > p2.name.comm) ? 1 : (p1.name.common < p2.name.common) ? -1 : 0);
}

export const appUtils = {
    sortedCountries,
    sortedDescCountries
};