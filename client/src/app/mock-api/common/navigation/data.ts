/* tslint:disable:max-line-length */
import { FuseNavigationItem } from '@fuse/components/navigation';

export const defaultNavigation: FuseNavigationItem[] = [
    {
        id: 'dashboard',
        title: 'Dashboard',
        type: 'basic',
        icon: 'heroicons_outline:home',
        link: '/dashboard'
    },
    {
        id: 'business',
        title: 'Business',
        type: 'basic',
        icon: 'heroicons_outline:office-building',
        link: '/business'
    },
    {
        id: 'properties',
        title: 'Properties',
        type: 'basic',
        icon: 'heroicons_outline:globe',
        link: '/properties',
        subtitle: "House, Land, Room, Appart..."
    },
    {
        id: 'users',
        title: 'Users',
        type: 'basic',
        icon: 'heroicons_outline:users',
        link: '/users',
        subtitle: "Business Users"
    },
    {
        id: 'gallery',
        title: 'Gallery',
        type: 'basic',
        icon: 'photo',
        link: '/gallery'
    },
    {
        id: 'settings',
        title: 'Settings',
        type: 'collapsable',
        icon: 'heroicons_outline:cog',
        link: '/settings',
        subtitle: "Business Settings",
        children: [
            {
                id: 'users-roles',
                title: 'Users Roles',
                type: 'basic',
                icon: '',
                link: '/users-roles',
            },
            {
                id: 'roles-access',
                title: 'Roles Access',
                type: 'basic',
                icon: '',
                link: '/roles-access',
            }
        ]
    }
];
