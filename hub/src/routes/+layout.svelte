<script lang="ts">
	import '../app.postcss';
	import { AppShell, AppBar } from '@skeletonlabs/skeleton';
	import { LightSwitch } from '@skeletonlabs/skeleton';
	import Navigation from '$lib/components/Navigation.svelte';
	import CCU from '$lib/components/CCU.svelte';

	// Floating UI for Popups
	import { computePosition, autoUpdate, flip, shift, offset, arrow } from '@floating-ui/dom';
	import { storePopup } from '@skeletonlabs/skeleton';
	storePopup.set({ computePosition, autoUpdate, flip, shift, offset, arrow });

	// Modals
	import { Modal } from '@skeletonlabs/skeleton';

	// Drawer for Navigation
	import { initializeStores, Drawer, getDrawerStore } from '@skeletonlabs/skeleton';
	initializeStores();

	const drawerStore = getDrawerStore();

	function drawerOpen(): void {
		drawerStore.open({});
	}
</script>

<Modal />

<Drawer>
	<h2 class="p-4 text-xl uppercase futurefrictions">Navigation</h2>
	<hr />
	<Navigation />
</Drawer>

<!-- App Shell -->
<AppShell slotSidebarLeft="bg-surface-500/5 w-0 lg:w-64">
	<svelte:fragment slot="header">
		<!-- App Bar -->
		<AppBar>
			<svelte:fragment slot="lead">
				<div class="flex items-center">
					<button class="lg:hidden btn btn-sm mr-4" on:click={drawerOpen}>
						<span>
							<svg viewBox="0 0 100 80" class="fill-token w-4 h-4">
								<rect width="100" height="10" />
								<rect y="30" width="100" height="10" />
								<rect y="60" width="100" height="10" />
							</svg>
						</span>
					</button>

					<img src="/images/pigeon-8.webp" width="100" class="px-5" alt="Pigeon" />
					<span class="text-xl uppercase futurefrictions"
						><a href="/">Future Frictions &gt; Hub</a></span
					>
				</div>
			</svelte:fragment>

			<svelte:fragment slot="trail">
				<LightSwitch />
				<CCU />
			</svelte:fragment>
		</AppBar>
	</svelte:fragment>

	<svelte:fragment slot="sidebarLeft">
		<Navigation />
	</svelte:fragment>

	<div class="container mx-auto p-8 space-y-8">
		<slot />
	</div>
</AppShell>

<style>
	.futurefrictions {
		font-family: 'Uni';
	}
</style>
