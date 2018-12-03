<template>
    <v-layout row>
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
    </v-layout>
</template>

<script src="./home.ts"></script>