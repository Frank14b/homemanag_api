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

// super admin navigation menu data

export const defaultAdminNavigation: FuseNavigationItem[] = [
    {
        id: 'dashboard',
        title: 'Dashboard',
        type: 'basic',
        icon: 'heroicons_outline:home',
        link: '/dashboard'
    },
    {
        id: "user-divider",
        title: "User Navigation",
        type: "divider"
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
    },
    {
        id: "admin-divider",
        title: "Admin Navigation",
        type: "divider"
    },
    {
        id: 'admin-properties',
        title: 'Properties',
        type: 'collapsable',
        icon: 'heroicons_outline:globe',
        link: '#',
        subtitle: "Properties Data & Settings",
        children: [
            {
                id: 'all-properties',
                title: 'All Properties',
                type: 'basic',
                icon: '',
                link: '/all-properties',
            },
            {
                id: 'property-types',
                title: 'Property Types',
                type: 'basic',
                icon: '',
                link: '/property/types',
            }
        ]
    }
];
