<template>
    <v-layout row wrap>
        <v-flex md6>
            <v-card>
                <v-card-title>
                    <div class="headline">
                        Line Chart
                    </div>
                </v-card-title>
                <v-sparkline :data="[0, 2, 5, 9, 5, 10, 3, 5, 0, 0, 1, 8, 2, 9, 0]"
                             :type="'trend'"
                             :gradient="['#42b3f4', '#42b3f4']"
                             :smooth="10"
                             :padding="8"
                             :line-width="2"
                             :stroke-linecap="'round'"
                             auto-draw></v-sparkline>
            </v-card>
        </v-flex>
        <v-flex md6>
            <v-card>
                <v-card-title>
                    <div class="headline">
                        Bar Chart
                    </div>
                </v-card-title>
                <v-sparkline :data="[0, 2, 5, 9, 5, 10, 3, 5, 6, 7, 1, 8, 2, 9, 0]"
                             :type="'bar'"
                             :gradient="['#42b3f4', '#42b3f4']"
                             :smooth="1"
                             :padding="8"
                             :line-width="7"
                             :stroke-linecap="'round'"
                             auto-draw></v-sparkline>
            </v-card>
        </v-flex>
        <v-flex md4>
            <v-card>
                <v-toolbar color="cyan" dark>
                    <v-toolbar-side-icon></v-toolbar-side-icon>

                    <v-toolbar-title>Inbox</v-toolbar-title>

                    <v-spacer></v-spacer>

                    <v-btn icon>
                        <v-icon>search</v-icon>
                    </v-btn>
                </v-toolbar>

                <v-list two-line>
                    <template v-for="(item, index) in items">
                        <v-subheader v-if="item.header"
                                     :key="item.header">
                            {{ item.header }}
                        </v-subheader>

                        <v-divider v-else-if="item.divider"
                                   :inset="item.inset"
                                   :key="index"></v-divider>

                        <v-list-tile v-else
                                     :key="item.title"
                                     avatar
                                     @click="">
                            <v-list-tile-avatar>
                                <img :src="item.avatar">
                            </v-list-tile-avatar>

                            <v-list-tile-content>
                                <v-list-tile-title v-html="item.title"></v-list-tile-title>
                                <v-list-tile-sub-title v-html="item.subtitle"></v-list-tile-sub-title>
                            </v-list-tile-content>
                        </v-list-tile>
                    </template>
                </v-list>
            </v-card>
        </v-flex>
        <v-flex md4>
            <v-data-iterator :items="dataIteratorItems"
                             :rows-per-page-items="rowsPerPageItems"
                             :pagination.sync="pagination"
                             content-tag="v-layout"
                             hide-actions
                             row
                             wrap>
                <v-toolbar slot="header"
                           class="mb-2"
                           color="indigo darken-5"
                           dark
                           flat>
                    <v-toolbar-title>This is a header</v-toolbar-title>
                </v-toolbar>

                <v-flex slot="item"
                        slot-scope="props"
                        xs12
                        sm6>
                    <v-card>
                        <v-card-title class="subheading font-weight-bold">{{ props.item.name }}</v-card-title>

                        <v-divider></v-divider>

                        <v-list dense>
                            <v-list-tile>
                                <v-list-tile-content>Calories:</v-list-tile-content>
                                <v-list-tile-content class="align-end">{{ props.item.calories }}</v-list-tile-content>
                            </v-list-tile>

                            <v-list-tile>
                                <v-list-tile-content>Fat:</v-list-tile-content>
                                <v-list-tile-content class="align-end">{{ props.item.fat }}</v-list-tile-content>
                            </v-list-tile>

                            <v-list-tile>
                                <v-list-tile-content>Carbs:</v-list-tile-content>
                                <v-list-tile-content class="align-end">{{ props.item.carbs }}</v-list-tile-content>
                            </v-list-tile>

                            <v-list-tile>
                                <v-list-tile-content>Protein:</v-list-tile-content>
                                <v-list-tile-content class="align-end">{{ props.item.protein }}</v-list-tile-content>
                            </v-list-tile>

                            <v-list-tile>
                                <v-list-tile-content>Sodium:</v-list-tile-content>
                                <v-list-tile-content class="align-end">{{ props.item.sodium }}</v-list-tile-content>
                            </v-list-tile>

                            <v-list-tile>
                                <v-list-tile-content>Calcium:</v-list-tile-content>
                                <v-list-tile-content class="align-end">{{ props.item.calcium }}</v-list-tile-content>
                            </v-list-tile>

                            <v-list-tile>
                                <v-list-tile-content>Iron:</v-list-tile-content>
                                <v-list-tile-content class="align-end">{{ props.item.iron }}</v-list-tile-content>
                            </v-list-tile>
                        </v-list>
                    </v-card>
                </v-flex>

                <v-toolbar slot="footer"
                           class="mt-2"
                           color="indigo"
                           dark
                           dense
                           flat>
                    <v-toolbar-title class="subheading">This is a footer</v-toolbar-title>
                </v-toolbar>
            </v-data-iterator>
        </v-flex>
        <v-flex md4></v-flex>
    </v-layout>
</template>

<script src="./home.ts"></script>