import NucleusComponentBase from '@/shared/application/nucleus-component-base';
import { Component } from 'vue-property-decorator';

@Component
export default class HomeComponent extends NucleusComponentBase {
    public get items() {
        return [
            { header: 'Today' },
            {
                avatar: 'https://cdn.vuetifyjs.com/images/lists/1.jpg',
                title: 'Brunch this weekend?',
                subtitle:
                    '<span class="text--primary">Ali Connors</span> &mdash; ' +
                        'I will be in your neighborhood doing errands this weekend. ' +
                        'Do you want to hang out?'
            },
            { divider: true, inset: true },
            {
                avatar: 'https://cdn.vuetifyjs.com/images/lists/2.jpg',
                title: 'Summer BBQ <span class="grey--text text--lighten-1">4</span>',
                subtitle:
                    '<span class="text--primary">to Alex, Scott, Jennifer</span> &mdash; ' +
                        'Wish I could come, but I am out of town this weekend.'
            },
            { divider: true, inset: true },
            {
                avatar: 'https://cdn.vuetifyjs.com/images/lists/3.jpg',
                title: 'Oui oui',
                subtitle:
                    '<span class="text--primary">Sandra Adams</span> &mdash; ' +
                        'Do you have Paris recommendations? Have you ever been?'
            },
            { divider: true, inset: true },
            {
                avatar: 'https://cdn.vuetifyjs.com/images/lists/4.jpg',
                title: 'Birthday gift',
                subtitle:
                    '<span class="text--primary">Trevor Hansen</span> &mdash; ' +
                        'Have any ideas about what we should get Heidi for her birthday?'
            },
            { divider: true, inset: true },
            {
                avatar: 'https://cdn.vuetifyjs.com/images/lists/5.jpg',
                title: 'Recipe to try',
                subtitle:
                    '<span class="text--primary">Britta Holt</span> &mdash; ' +
                        'We should eat this: Grate, Squash, Corn, and tomatillo Tacos.'
            }
        ];
    }

    public rowsPerPageItems = [4, 8, 12];
    public pagination = {
        rowsPerPage: 4
    };

    public get dataIteratorItems() {
        return [
            {
                value: false,
                name: 'Frozen Yogurt',
                calories: 159,
                fat: 6.0,
                carbs: 24,
                protein: 4.0,
                sodium: 87,
                calcium: '14%',
                iron: '1%'
            },
            {
                value: false,
                name: 'Ice cream sandwich',
                calories: 237,
                fat: 9.0,
                carbs: 37,
                protein: 4.3,
                sodium: 129,
                calcium: '8%',
                iron: '1%'
            }
        ];
    }

    public get years() {
        return [
            {
                color: 'cyan',
                year: '1960'
            },
            {
                color: 'green',
                year: '1970'
            },
            {
                color: 'pink',
                year: '1980'
            },
            {
                color: 'amber',
                year: '1990'
            },
            {
                color: 'orange',
                year: '2000'
            }
        ];
    }
}